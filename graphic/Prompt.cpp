#include "Prompt.hpp"

Prompt::Prompt()
 : status(false), x(500), y(600), curr(), old()
{}

Prompt::Prompt(const unsigned int &x, const unsigned int &y)
 : status(false), x(x), y(y), curr(), old()
{}

void Prompt::setStatus()
{
	this->status = !this->status;
}

void Prompt::setStatus(const bool &status)
{
	this->status = status;
}

bool Prompt::getStatus() const
{
	return this->status;
}

void Prompt::setX(const unsigned int &x)
{
	this->x = x;
}

void Prompt::setY(const unsigned int &y)
{
	this->y = y;
}

void Prompt::setOld(const std::string &str)
{
	this->old = str;
}

std::string Prompt::getOld() const
{
	return this->old;
}

void Prompt::printText(const unsigned int &x, const unsigned int &y, const std::string &str) const
{
	glColor4f(1.0, 1.0, 1.0, 1.0);
	glRasterPos2i(x, y);
	glDisable(GL_TEXTURE);
	//glDisable(GL_TEXTURE_2D);

	for (const char c : str)
		glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c);

	glEnable(GL_TEXTURE_2D);
	//glEnable(GL_TEXTURE);
}

void Prompt::doublePrint() const
{
	if (!this->curr.empty() || !this->old.empty())
	{
		printText(this->x, this->y + 25, this->curr);
		printText(this->x, this->y, this->old);
	}
}

void Prompt::parser(const std::string &str)
{
	std::string tmp = str;

	std::cout << tmp << std::endl;

	if (str == "return")
	{
		this->old = this->curr;
		this->curr = "";
		glClear(GL_COLOR_BUFFER_BIT);
		tmp = "";
	}
	else if (str == "space")
		tmp = " ";
	else if (str == "backspace")
	{
		glClear(GL_COLOR_BUFFER_BIT);
		if (this->curr.length() > 0)
			this->curr.pop_back();
		tmp = "";
	}
	else if (str.length() == 3 && str.at(0) == '[' && str.at(2) == ']')
		tmp = str.at(1);
	else if (str.length() > 1)
		tmp = "";

	this->curr += tmp;
	this->doublePrint();
}
