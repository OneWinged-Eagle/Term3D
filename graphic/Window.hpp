#pragma once

#include "core/signalsUtils.hpp"

#include "graphic/Prompt.hpp"
//#include "core/Core.hpp" // à enlever plus tard (threading, tout ça, toussa)

#include <boost/signals2.hpp>

class Window : public Observable<WindowObservers>
{
private:
	bool running;
	float width, height;

//	Core core; // Idem, à enlever
	Prompt prompt;

	SDL_Event event;
	SDL_Surface *screen;
	Uint32 start;

public:
	Window();
	Window(const unsigned int &width, const unsigned int &height);

	unsigned int getWidth();
	unsigned int getHeight();

	void stop(const std::string &error);
	void setPrompt();
	bool checkEvent();
	void draw() const;
	void looper();
	void init() const;
	void launch();

	void operator()();
};
