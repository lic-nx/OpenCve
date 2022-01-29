//2.Разработать программу рисования треугольников с сохранением результата в файле.
#include <opencv2/opencv.hpp>
	#include <cstdlib>
	#include <iostream>
//solution 1 Разработать программу рисования треугольников с сохранением результата в файле.
	using namespace cv;
	void my_mouse_callback(int event, int x, int y, int flags, void* param);
Point point1(0,0);
Point point2(0, 0);
Point point3(0, 0);
int c = 0;

void draw_box(Mat* pimg)
{
	line(*pimg, point1, point2, Scalar(0x00, 0x00, 0x00));
	line(*pimg, point2, point3, Scalar(0x00, 0x00, 0x00));
	line(*pimg, point1, point3, Scalar(0x00, 0x00, 0x00));
}
int main(int argc, char* argv[])
{

	Mat image = Mat(Size(400, 400), CV_8UC3,
		Scalar(0xff, 0xff, 0xff));
	Mat temp = image.clone();
	namedWindow("Box Example");
	setMouseCallback("Box Example", my_mouse_callback,
		(void*)&image);
	while (1)
	{
		image.copyTo(temp);
		imshow("Box Example", temp);
		if (waitKey(15) == 27) break;
	}
	imwrite("C:\\Users\\nasty\\source\\repos\\progectOpencv\\progectOpencv\\opencvMyImage.jpg", temp);
	destroyWindow("Box Example");

}
void my_mouse_callback(int event, int x, int y,
	int flags, void* param)
{
	Mat* pimg = (Mat*)param;
	switch (event)
	{
	
	case EVENT_LBUTTONUP:
		if (c == 0)
		{
			point1.x = x;
			point1.y = y;
			c = 1;
		}
		else if (c == 1)
		{
			point2.x = x;
			point2.y = y;
			c = 2;
		}
		else if (c == 2)
		{
			point3.x = x;
			point3.y = y;
			draw_box(pimg);
			c = 0;

		}

	}
}
