#ifndef WINDOW_H
#define WINDOW_H

#include <iostream>
#include <string>

#include <GL/gl.h>
#include <GL/glut.h>

#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include "Prompt.h"
#include "../core/Core.hpp"

class Window
{
private:
  float		width;
  float		height;
  Prompt	*_prompt;
  SDL_Surface	*screen;
  bool		running;
  Uint32	start;
  SDL_Event	event;
  Core		core;

public:
  Window();
  Window(float width, float height);
  float getWidth();
  float getHeight();
  void Start(int ac, char **av);
  void Draw();
  bool isPrompt();
  void setPrompt();
  void doPrompt(std::string str);
  bool checkEvent(SDL_Event * event);
  void Looper();
  void Stop(std::string error);
  void Init();
};


#endif
