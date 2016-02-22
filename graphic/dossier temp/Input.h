#ifndef _INPUT
#define	_INPUT

#include <SDL.h>

class Input
{
public:
	Input();
	~Input();

	void updateEvent();
	bool end() const;
	void displayCursor(bool response) const;
	void getCursor(bool response) const;

	bool getInput(const SDL_Scancode Input) const;
	bool getMouseButton(const Uint8 button) const;
	bool getMouseMove() const;

	int getX() const;
	int getY() const;

	int getXRel() const;
	int getYRel() const;


private:
	SDL_Event m_events;
	bool m_inputs[SDL_NUM_SCANCODES];
	bool m_mouseButton[8];

	int m_x;
	int m_y;
	int m_xRel;
	int m_yRel;

	bool m_end;
};

#endif // !_INPUT
