#ifndef PROMPT_H
#define PROMPT_H

#include <iostream>
#include <string>

#include <GL/gl.h>
#include <GL/glut.h>

#include <SDL/SDL.h>
#include <SDL/SDL_image.h>


class Prompt
{
 private:

  float _x;
  float _y;
  std::string old;
  std::string curr;
  bool status;

 public:

  Prompt();
  Prompt(float sx, float sy);
  std::string getComm(void);
  void setComm(std::string str);//const
  void setX(float x);
  void setY(float y);
  bool getStatus();
  void setStatus();
  void parser(std::string str);
  void printText(float x, float y, std::string string);
  void doublePrint();
  std::string getOld();
};

#endif
