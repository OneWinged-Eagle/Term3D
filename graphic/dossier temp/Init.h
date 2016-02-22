#ifndef _INIT
#define _INIT

#include <GL\glew.h>

#include <glm\glm.hpp>
#include <glm\gtx\transform.hpp>
#include <glm\gtc\type_ptr.hpp>

#include <SDL.h>

#include <iostream>
#include <string>

#include "Input.h"
#include "Shader.h"
#include "Camera.h"
#include "Cube.h"
#include "Sol.h"

class Init
{
public:
	Init(std::string title, int windowWidth, int windowHeight);
	~Init();

	bool windowInit();
	bool GLInit();
	void mainLoop();

private:
	std::string	m_title;
	int m_windowWidth;
	int m_windowHeight;

	SDL_Window *m_window;
	SDL_GLContext m_openGLContext;
	SDL_Event m_events;
	Input	m_input;
};

#endif // !_INIT
