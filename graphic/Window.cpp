#include "Window.hpp"

Window::Window()
 : running(true), height(640), width(480), /*core(),*/ prompt(), event(), screen(SDL_SetVideoMode(this->width, this->height, 24, SDL_SWSURFACE | SDL_OPENGL)), start()
{}

Window::Window(const unsigned int &height, const unsigned int &width)
: running(true), height(height), width(width), /*core(),*/ prompt(), event(), screen(SDL_SetVideoMode(this->width, this->height, 24, SDL_SWSURFACE | SDL_OPENGL)), start()
{}

unsigned int Window::getHeight()
{
	return this->height;
}

unsigned int Window::getWidth()
{
	return this->width;
}

void Window::stop(const std::string &error)
{
	std::cerr << error << std::endl;
	this->running = false;
}

void Window::setPrompt()
{
	if (this->prompt.getStatus())
	{
		std::cout << "La commande est : \"" << this->prompt.getOld() << "\"" << std::endl;
		this->Notify<WindowObservers::Exec>(this->prompt.getOld());
//		this->core.exec(this->prompt.getOld()); //CORE STRING PROMPT
		this->prompt.setOld("");
	}
	this->prompt.setStatus();
}

bool Window::checkEvent()
{
	std::string str;

	if (this->event.type == SDL_QUIT)
	{
		this->stop("Thanks for testing Term3D !");
		return false;
	}
	else if (this->event.type == SDL_KEYUP)
	{
		str = std::string(SDL_GetKeyName(this->event.key.keysym.sym));
		if (this->prompt.getStatus())
			this->prompt.parser(str);
		if (str == "escape")
		{
			this->stop("Thanks for testing Term3D !");
			return false;
		}
		if (str == "return")
			this->setPrompt();
	}
	return true;
}

void Window::draw() const
{
	if (this->prompt.getStatus())
		this->prompt.doublePrint();
	glBegin(GL_QUADS);
	glEnd();
	glFlush();
}

void Window::looper(void)
{
	while(this->running)
	{
		this->start = SDL_GetTicks();
		this->draw();
		while (SDL_PollEvent(&this->event) && this->checkEvent());
//		SDL_GL_SwapBuffers(); // Fait Segfault Willy
		if (1000 / 60 > (SDL_GetTicks() - start)) //KEYWORD 1000 = SPEED
			SDL_Delay(1000 / 60 - (SDL_GetTicks() - start));
	}
	SDL_Quit();
}

void Window::init() const
{
	glClearColor(0.0, 0.0, 0.0, 0.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0.0, 1280, 720, 1.0, -1.0, 1.0);
	glEnable(GL_BLEND);
	glEnable(GL_TEXTURE_2D);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
}

void Window::launch()
{
	int argcp = 0;
	char **argv = NULL;

	glutInit(&argcp, argv);
	if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
		this->stop("Error Init SDL");
	this->init();
	this->looper();
}

void Window::operator()()
{
	this->launch();
}
