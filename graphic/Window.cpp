#include "Window.h"

Window::Window()
{
  this->width = 640;
  this->height = 480;
  this->screen = SDL_SetVideoMode(this->width, this->height, 24, SDL_SWSURFACE|SDL_OPENGL);
  this->running = true;
  this->_prompt = new Prompt();
}

Window::Window(float _width, float _height)
{
  
  if (_width <= 0 || _height <= 0)
    {
      this->width = 640;
      this->height = 480;
    }
  else
    {
      this->width = _width;
      this->height = _height;
    }
  this->screen = SDL_SetVideoMode(this->width, this->height, 24, SDL_SWSURFACE|SDL_OPENGL);
  this->running = true;
  this->_prompt = new Prompt();
}

void Window::Start(int argc, char **argv)
{
  glutInit(&argc, argv);
  if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
    this->Stop("Error Init SDL");
  this->Init();
  this->Looper();
  //std::cout << "END LOOP" << std::endl;//segfault avant?
}

void Window::Draw()
{
  if (this->isPrompt())
    this->_prompt->doublePrint();
  glBegin(GL_QUADS);
  glEnd();
  glFlush();
}

float Window::getWidth()
{
  return (this->width);
}

float Window::getHeight()
{
  return (this->height);
}

bool Window::isPrompt()
{
  return (this->_prompt->getStatus());
}

void Window::doPrompt(std::string str)
{
  this->_prompt->parser(str);
}

void Window::setPrompt()
{
  if (this->_prompt->getStatus())
    {
      this->core.exec(this->_prompt->getComm());//CORE STRING PROMPT
      this->_prompt->setComm("");
    }
  this->_prompt->setStatus();
}

bool Window::checkEvent(SDL_Event * event)
{
  std::string str;

  if (event->type == SDL_QUIT)
    {
      Stop("Thanks for testing Term3D !");
      return false;
    }
  else if (event->type == SDL_KEYUP)
    {
      str = std::string(SDL_GetKeyName(event->key.keysym.sym));
      if (this->isPrompt())
	this->doPrompt(str);
      if (str == "escape")
	{
	  Stop("Thanks for testing Term3D !");
	  return false;
	}
      if (str == "return")
	setPrompt();
    }
  return true;
}

void Window::Looper(void)
{

  while(this->running)
    {
      this->start = SDL_GetTicks();
      this->Draw();
      while (SDL_PollEvent(&event))
	{
	  if (this->checkEvent(&event) != true)
	    break;
	}
      SDL_GL_SwapBuffers();
      if (1000/60 > (SDL_GetTicks()-start))//KEYWORD 1000 = SPEED
	SDL_Delay(1000/60-(SDL_GetTicks()-start));
    }
  SDL_Quit();
}

void Window::Stop(std::string error)
{
  std::cerr << error << std::endl;
  this->running = false;
}

void Window::Init(void)
{
  glClearColor(0.0, 0.0, 0.0, 0.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  glOrtho(0.0, 1280, 720, 1.0, -1.0, 1.0);
  glEnable(GL_BLEND);
  glEnable(GL_TEXTURE_2D);
  glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
}
