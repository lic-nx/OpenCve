//3.Разработать программу создания и сохранения изображения, являющегося выделенной с помощью мыши прямоугольной областью другого изображения. 
#include <opencv2/opencv.hpp>
#include <cstdlib>
#include <iostream>
#include <vector>
using namespace cv;
using namespace std;

void my_mouse_callback(int event, int x, int y, int flags, void* param);

Rect box = Rect(-1, -1, 0, 0);

bool drawing_box = false;
bool show_rect = false;
void draw_box(Mat* pimg, Rect rect)
{
	rectangle(
		*pimg,
		Point(box.x, box.y),
		Point(box.x + box.width, box.y + box.height),
		Scalar(0x00, 0x00, 0x00));
}

int main(int argc, char* argv[])
{
	Mat image = imread("test.jpg", 1);
	Mat temp = image.clone();
	int cols = 0;
	int rows = 0;
	int secondRows = 0;
	namedWindow("Box Example");
	setMouseCallback("Box Example", my_mouse_callback, (void*)&image);

	Mat time = image.clone();
	Mat test(1, 1, CV_8UC1);
	Mat res;
	while (1)
	{
		image.copyTo(temp);
		if (drawing_box)
		{
			draw_box(&temp, box);

		}

		imshow("Box Example", temp);

		show_rect = false;
		if (waitKey(15) == 27) break;
	}


	Mat ROI(temp, box);

	destroyWindow("Box Example");

	imshow("Result", ROI); waitKey();
	//imwrite("C:\Users\nasty\source\repos\progectOpencv\progectOpencv\\newImage.png", croppedImage);
}
void my_mouse_callback(int event, int x, int y, int flags, void* param)
{
	Mat* pimg = (Mat*)param;
	switch (event)
	{
	case EVENT_MOUSEMOVE:
		if (drawing_box)
		{
			box.width = x - box.x;
			box.height = y - box.y;
		}
		break;

	case EVENT_LBUTTONDOWN:
		drawing_box = true;
		box = Rect(x, y, 0, 0);
		break;
	case EVENT_LBUTTONUP:
		drawing_box = false;
		if (box.width < 0)
		{
			box.x += box.width;
			box.width *= -1;
		}
		if (box.height < 0)
		{
			box.y += box.height;
			box.height *= -1;
		}
		draw_box(pimg, box);
		show_rect = true;
		break;
	}
}
