#include "../core/Core.hpp"

#include <GL/gl.h>
#include <GL/glut.h>

#include <SDL/SDL.h>
#include <SDL/SDL_image.h>

#include <stdio.h> //for debug

#ifndef GL_UNSIGNED_SHORT_5_6_5
#define GL_UNISGNED_SHORT_5_6_5 0x8363
#endif

#ifndef GL_CLAMP_TO_EDGE
#define GL_CLAMP_TO_EDGE 0x812F
#endif

static float sx = 500;
static float sy = 600;
static std::string curr = "";
static std::string old = "";

static Core core;

void renderBitmapString(float x, float y, std::string string)
{
  glColor4f(1.0,1.0,1.0,1.0);
  glRasterPos2i(x,y);
  glDisable(GL_TEXTURE);
  //glDisable(GL_TEXTURE_2D);

  for (unsigned int i = 0; i < string.length(); i++)
    glutBitmapCharacter(GLUT_BITMAP_9_BY_15, (int)string[i]);
  glEnable(GL_TEXTURE_2D);
  //glEnable(GL_TEXTURE);
}

void buffering(std::string string)
{
  std::string tmp;
  if (string == "return")
    {
      tmp = old;
      old = curr;
      curr = "";
      glClear(GL_COLOR_BUFFER_BIT);
			std::cout << old << std::endl;////////////////// LA CHAINE ECRITE EST PRINT ICI

			//
			core.exec(old);
			//

    }
  if (string == "space")
    string = " ";
  if (string.length() > 1 )
    string = "";

  curr = curr + string;
  renderBitmapString(sx, sy + 25, curr);
  renderBitmapString(sx, sy, old);
}

void init(void)
{
  glClearColor(0.0, 0.0, 0.0, 0.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  glOrtho(0.0, 1280, 720, 1.0, -1.0, 1.0);
  glEnable(GL_BLEND);
  glEnable(GL_TEXTURE_2D);
  glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
}

void draw()
{
  if (old != "" || curr != ""){
    renderBitmapString(sx, sy + 25, curr);
    renderBitmapString(sx, sy, old);
  }
  //  glClear(GL_COLOR_BUFFER_BIT);
  glBegin(GL_QUADS);

  //POUR AVOIR UN JOLIE FOND
  /*  glTexCoord2f(0,0);
  glVertex2f(0,600);

  glTexCoord2f(1,0);
  glVertex2f(500,600);

  glTexCoord2f(1,1);
  glVertex2f(500,700);

  glTexCoord2f(0,1);
  glVertex2f(0,700);*/

  glEnd();
  glFlush();
}

int main(int argc, char **argv)
{
  SDL_Surface *screen;
  bool running = true;
  Uint32 start;
  SDL_Event event;
  std::string str;

  //  printf("hehe");
  glutInit(&argc, argv);
  if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
    {
      std::cerr << "Error Init SDL" << std::endl;
      return (1);
    }
    screen = SDL_SetVideoMode(640, 480,24, SDL_SWSURFACE|SDL_OPENGL);
		(void)screen;
    init();
    while(running)
      {
	start = SDL_GetTicks();
	draw();
	while (SDL_PollEvent(&event))
	  {
	    if (event.type == SDL_QUIT)
	      running = false;
	    else if (event.type == SDL_KEYUP)
	      {
		//std::cout << SDL_GetKeyName(event.key.keysym.sym) << std::endl;
		str = std::string(SDL_GetKeyName(event.key.keysym.sym));
		if (str == "escape")
		  {
		    running = false;
		    break;
		  }
		buffering(str);
	      }
	  }
	SDL_GL_SwapBuffers();
	if (1000/60 > (SDL_GetTicks()-start))
	  SDL_Delay(1000/60-(SDL_GetTicks()-start));
      }
    SDL_Quit();
    return (0);
}
