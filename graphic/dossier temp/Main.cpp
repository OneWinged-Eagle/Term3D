#include "Init.h"

int main(int argc, char **argv)
{
	Init term3D("Term3D", 1024, 768);
	if (term3D.windowInit() == false)
		return -1;
	if (term3D.GLInit() == false)
		return -1;

	term3D.mainLoop();

	return 0;
}




















/*#include <Windows.h>

#include <SDL.h>
#include <iostream>


#include <GL\glew.h>

int main(int argc, char **argv)
{
	SDL_Window *window;
	SDL_Event events;
	bool	running;

	SDL_GLContext contextOpenGL;


	if (SDL_Init(SDL_INIT_VIDEO) < 0)
	{
		std::cout << "sdl init error : " << SDL_GetError() << std::endl;
		return -1;
	}

	//version OPENGL
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 1);

	//DOUBLE BUFFER
	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
	SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE, 24);


	window = SDL_CreateWindow(
		"Term3D",
		SDL_WINDOWPOS_UNDEFINED,
		SDL_WINDOWPOS_UNDEFINED,
		640,
		480,
		SDL_WINDOW_SHOWN | 
		SDL_WINDOW_OPENGL
		);

	if (window == NULL)
	{
		std::cout << "Could not create window: %s" << SDL_GetError() << std::endl;
		return -1;
	}

	//set context opengl
	contextOpenGL = SDL_GL_CreateContext(window);
	if (contextOpenGL == 0)
	{
		std::cout << SDL_GetError() << std::endl;
		SDL_DestroyWindow(window);
		SDL_Quit();
		return -1;
	}

	//init de glew
	GLenum initGLEW(glewInit());

	if (initGLEW != GLEW_OK)
	{
		std::cout << "glew inti error : " << glewGetErrorString(initGLEW) << std::endl;

		SDL_GL_DeleteContext(contextOpenGL);
		SDL_DestroyWindow(window);
		SDL_Quit();

		return -1;
	}


	running = true;
	float vertices[] = { -0.5, -0.5,   0.0, 0.5,   0.5, -0.5 };


	while (running)
	{
		SDL_WaitEvent(&events);
		if (events.window.event == SDL_WINDOWEVENT_CLOSE)
			running = false;
		
		// Nettoyage de l'écran

		glClear(GL_COLOR_BUFFER_BIT);


		// On remplie puis on active le tableau Vertex Attrib 0

		glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 0, vertices);
		glEnableVertexAttribArray(0);


		// On affiche le triangle

		glDrawArrays(GL_TRIANGLES, 0, 3);


		// On désactive le tableau Vertex Attrib puisque l'on n'en a plus besoin

		glDisableVertexAttribArray(0);


		// Actualisation de la fenêtre

		SDL_GL_SwapWindow(window);

		



		//glClear(GL_COLOR_BUFFER_BIT);
		//SDL_GL_SwapWindow(window);
	}

	SDL_GL_DeleteContext(contextOpenGL);
	SDL_DestroyWindow(window);
	SDL_Quit();
	return 0;
}*/