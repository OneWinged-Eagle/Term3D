#pragma once

#include "core/CommandHandler.hpp"
#include "core/signalsUtils.hpp"

//#include "graphic/Graphic.hpp"
#include "graphic/Window.hpp"

//#include "network/Network.hpp"

#include <boost/algorithm/string.hpp>
#include <boost/signals2.hpp>
#include <boost/thread.hpp>

/*bool g_cmdReady;
boost::mutex g_mutex;
boost::condition_variable g_cmdCond;
std::string g_cmd;
boost::mutex g_cmdMutex;*/

class Core
{
private:
	CommandHandler commandHandler;

	Window window;

	boost::thread graphicThread;

public:
	Core();

	void launch();

private:
	void exec(const std::string &cmd) const;
	void parse(const std::string &str, std::string &cmd, pathVector &paths) const;
};
