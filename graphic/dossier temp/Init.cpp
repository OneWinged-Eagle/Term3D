#include "Init.h"

Init::Init(std::string title, int windowWidth, int windowHeight)
	:m_title(title), m_windowWidth(windowWidth), m_windowHeight(windowHeight), m_window(0), m_openGLContext(0)
{

}

Init::~Init()
{
	SDL_GL_DeleteContext(m_openGLContext);
	SDL_DestroyWindow(m_window);
	SDL_Quit();
}

bool	Init::windowInit()
{
	if (SDL_Init(SDL_INIT_VIDEO) < 0)
	{
		std::cout << "sdl init error : " << SDL_GetError() << std::endl;
		SDL_Quit();
		return false;
	}

	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 1);

	//double buffering

	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
	SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE, 24);

	m_window = SDL_CreateWindow(
		m_title.c_str(),
		SDL_WINDOWPOS_UNDEFINED,
		SDL_WINDOWPOS_UNDEFINED,
		m_windowWidth,
		m_windowHeight,
		SDL_WINDOW_SHOWN |
		SDL_WINDOW_OPENGL
		);

	if (m_window == 0)
	{
		std::cout << "error creating window" << SDL_GetError() << std::endl;
		SDL_Quit();

		return false;
	}

	//set openglcontext
	m_openGLContext = SDL_GL_CreateContext(m_window);

	if (m_openGLContext == 0)
	{
		std::cout << SDL_GetError() << std::endl;
		SDL_DestroyWindow(m_window);
		SDL_Quit();


		return false;
	}
	return true;
}

bool Init::GLInit()
{
	GLenum GLEWInit(glewInit());

	if (GLEWInit != GLEW_OK)
	{
		std::cout << "glew init error : " << glewGetErrorString(GLEWInit) << std::endl;
		SDL_GL_DeleteContext(m_openGLContext);
		SDL_DestroyWindow(m_window);
		SDL_Quit();

		return false;
	}
	return true;
}

void Init::mainLoop()
{

	//test
	unsigned int frameRate;
	Uint32 debutBoucle, finBoucle, tempsEcoule;


	frameRate = 1000 / 50;
	debutBoucle = 0;
	finBoucle = 0;
	tempsEcoule = 0;

	//matrices

	glm::mat4 projection;
	glm::mat4 modelview;

	projection = glm::perspective(70.0, (double)m_windowWidth /m_windowHeight, 1.0, 100.0);
	modelview = glm::mat4(1.0);

	//set de la cam qui bouuuuge
	Camera camera(glm::vec3(3, 4, 10), glm::vec3(0, 2.1, 2), glm::vec3(0, 1, 0), 0.5, 0.5);
	m_input.displayCursor(false);
	m_input.getCursor(true);

	//set cube
	Cube cube(2.0, "Shaders/couleur3D.vert", "Shaders/couleur3D.frag");
	Cube cubetest(2.0, "Shaders/couleur3D.vert", "Shaders/couleur3D.frag");

	//set du sol
	Sol solHerbe(30.0, 30.0, 30, 30, "Shaders/texture.vert", "Shaders/texture.frag", "Textures/Herbe.tga");


	while (m_input.end() == false)
	{
		//test
		debutBoucle = SDL_GetTicks();




		m_input.updateEvent();
		if (m_input.getInput(SDL_SCANCODE_ESCAPE))
			break; //a changer SAAAAAAAAAAALE

		camera.move(m_input);

		// Nettoyage de l'écran

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		//gestion cam
		camera.lookAt(modelview);



		


		//rendering

		glm::mat4 modelviewSave = modelview;

		modelview = translate(modelview, glm::vec3(0, -0.01, 0));
		solHerbe.display(projection, modelview);

		modelview = modelviewSave;

		
		
		
		modelviewSave = modelview;

		modelview = translate(modelview, glm::vec3(-2.5, 1, -3));
		cube.display(projection, modelview);

		modelview = modelviewSave;
		modelviewSave = modelview;


		modelview = translate(modelview, glm::vec3(-10, 1, -3));
		cube.display(projection, modelview);

		modelview = modelviewSave;













		// Actualisation de la fenêtre

		SDL_GL_SwapWindow(m_window);

		//test
		finBoucle = SDL_GetTicks();
		 tempsEcoule = finBoucle - debutBoucle;

		if(tempsEcoule < frameRate)
		  SDL_Delay(frameRate - tempsEcoule);


		//glClear(GL_COLOR_BUFFER_BIT);
		//SDL_GL_SwapWindow(window);
	}
}