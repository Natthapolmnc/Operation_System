import cv2 as cv
import time

img=cv.imread("test.jpg",0)
for i in range(len(img)):
    for j in range(len(img[0])):
        try:
            old=img[i][j]
            new=(round((old/255),0))*255
            img[i][j]=new
            err=old-new
            img[i][j+1] = img[i][j+1] + err*(7/16)
            img[i+1][j-1] = img[i+1][j-1]+err*(3/16)
            img[i+1][j] = img[i+1][j]+err*(5/16)
            img[i+1][j+1] = img[i+1][j+1]+err*(1/16)
        except:
            continue
cv.imwrite("res_test.jpg",img)