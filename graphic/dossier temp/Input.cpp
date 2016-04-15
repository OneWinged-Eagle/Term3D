#include "Input.h"

Input::Input()
	: m_x(0), m_y(0), m_xRel(0), m_yRel(0), m_end(false)
{
	//init du array d'inputs clavier
	for (int i = 0; i < SDL_NUM_SCANCODES; i++)
		m_inputs[i] = false;

	//init du array d'input souris
	for (int i = 0; i < 8; i++)
		m_mouseButton[i] = 0;
}

Input::~Input()
{

}

void	Input::updateEvent()
{
	m_xRel = 0;
	m_yRel = 0;

	while (SDL_PollEvent(&m_events))
	{
		switch (m_events.type)
		{
		case SDL_KEYDOWN: //touche enfoncé
			m_inputs[m_events.key.keysym.scancode] = true;
		break;
		
		case SDL_KEYUP: //touche relachée
			m_inputs[m_events.key.keysym.scancode] = false;
			break;

		case SDL_MOUSEBUTTONDOWN: //bouton souris enfoncé
			m_mouseButton[m_events.button.button] = true;
			break;

		case SDL_MOUSEBUTTONUP: //bouton souris relaché
			m_mouseButton[m_events.button.button] = false;
			break;

		case SDL_MOUSEMOTION: //mouve de sourus
			m_x = m_events.motion.x;
			m_y = m_events.motion.y;

			m_xRel = m_events.motion.xrel;
			m_yRel = m_events.motion.yrel;
			break;

		case SDL_WINDOWEVENT: //event fermeture fenetre
			if (m_events.window.event == SDL_WINDOWEVENT_CLOSE)
				m_end = true;
			break;

		default:
			break;
		}	
	}
}

bool	Input::end() const
{
	return m_end;
}

void	Input::displayCursor(bool response) const
{
	if (response)
		SDL_ShowCursor(SDL_ENABLE);
	else
		SDL_ShowCursor(SDL_DISABLE);
}

void	Input::getCursor(bool response) const
{
	if (response)
		SDL_SetRelativeMouseMode(SDL_TRUE);
	else
		SDL_SetRelativeMouseMode(SDL_FALSE);
}


bool	Input::getInput(const SDL_Scancode Input) const
{
	return m_inputs[Input];
}

bool	Input::getMouseButton(const Uint8 button) const
{
	return m_mouseButton[button];
}

bool	Input::getMouseMove() const
{
	if (m_xRel == 0 && m_yRel == 0)
		return false;
	else
		return true;
}

int		Input::getX() const
{
	return m_x;
}

int		Input::getY() const
{
	return m_y;
}

int		Input::getXRel() const
{
	return m_xRel;
}

int		Input::getYRel() const
{
	return m_yRel;
}