import time
from absl import app, flags, logging
from absl.flags import FLAGS
import time
import os
import cv2
import datetime
import numpy as np
import tensorflow as tf
from yolov3_tf2.models import (
    YoloV3, YoloV3Tiny
)
from yolov3_tf2.dataset import transform_images, load_tfrecord_dataset
from yolov3_tf2.utils import draw_outputs
from yolov3_tf2.utils import save_coords

flags.DEFINE_string('classes', './data/coco.names', 'path to classes file')
flags.DEFINE_string('weights', './checkpoints/yolov3.tf',
                    'path to weights file')
flags.DEFINE_boolean('tiny', False, 'yolov3 or yolov3-tiny')
flags.DEFINE_integer('size', 416, 'resize images to')
flags.DEFINE_string('image', './data/girl.png', 'path to input image')
flags.DEFINE_string('tfrecord', None, 'tfrecord instead of image')
flags.DEFINE_string('output', './output.jpg', 'path to output image')
flags.DEFINE_string('output_dir', './result', 'path to output folder')
flags.DEFINE_integer('num_classes', 80, 'number of classes in the model')

def monitor_photo(yolo, class_names, image_file, output_dir):
    if os.path.isfile(image_file):
        img_raw = tf.image.decode_image(
                open(image_file, 'rb').read(), channels=3)

        img = tf.expand_dims(img_raw, 0)
        img = transform_images(img, FLAGS.size)

        t1 = time.time()
        boxes, scores, classes, nums = yolo(img)
        wh = np.flip(img.shape[0:2])
        t2 = time.time()
        logging.info('time: {}'.format(t2 - t1))
        logging.info('detections:')
        for i in range(nums[0]):
            logging.info('\t{}, {}, {}'.format(class_names[int(classes[0][i])],
                                            np.array(scores[0][i]),
                                            np.array(boxes[0][i])))

        img = cv2.cvtColor(img_raw.numpy(), cv2.COLOR_RGB2BGR)
        img = draw_outputs(img, (boxes, scores, classes, nums), class_names)

        st = datetime.datetime.now()
        s = st.strftime('%Y-%m-%d-%H-%M-%S')

        imageFileName = output_dir + s + '.jpg'
        cv2.imwrite(imageFileName, img)

        coordFileName = output_dir + s + '.txt'
        file_txt = open(coordFileName, "w") 
        save_coords(file_txt, img, (boxes, scores, classes, nums), class_names)
        file_txt.close()

        os.remove(image_file)

        logging.info('output saved to: {}, {}'.format(imageFileName, coordFileName))
        return 1
    else:
        logging.info('no input')
        return 0

# python monitor.py --image ./data/parking.png --output_dir ./result/

def main(_argv):
    #physical_devices = tf.config.experimental.list_physical_devices('GPU')
    #for physical_device in physical_devices:
    #    tf.config.experimental.set_memory_growth(physical_device, True)

    logging.info(FLAGS.tiny)
    if FLAGS.tiny:        
        yolo = YoloV3Tiny(classes=FLAGS.num_classes)
    else:
        yolo = YoloV3(classes=FLAGS.num_classes)

    yolo.load_weights(FLAGS.weights).expect_partial()
    logging.info('weights loaded')

    class_names = [c.strip() for c in open(FLAGS.classes).readlines()]
    logging.info('classes loaded')

    timeout = 1
    while True :
        monitor_photo(yolo, class_names, FLAGS.image, FLAGS.output_dir)
        logging.info('timeout...')
        time.sleep(timeout)
    
if __name__ == '__main__':
    try:
        app.run(main)
    except SystemExit:
        pass
