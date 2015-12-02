#pragma once

#include <GL/gl.h>
#include <GL/glut.h>

#include <SDL/SDL.h>
#include <SDL/SDL_image.h>

#include <iostream>
#include <string>

class Prompt
{
	#define X 500
	#define Y 600

private:
	bool status;
	unsigned int x, y;
	std::string curr, old;

public:
	Prompt();
	Prompt(const unsigned int &x, const unsigned int &y);

	void setStatus();
	void setStatus();
	bool getStatus() const;
	void setX(const unsigned int &x);
	void setY(const unsigned int &y);
	void setOld(const std::string &str);
	std::string getOld() const;

	void printText(const unsigned int &x, const unsigned int &y, const std::string &str) const;
	void doublePrint() const;
	void parser(const std::string &str);
};
