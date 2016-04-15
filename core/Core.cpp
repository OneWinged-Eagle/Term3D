#include "Core.hpp"

Core::Core()
 : commandHandler(), window()
{
	this->window.Register<WindowObservers::Exec>([this](const std::string &str)
	{
		this->exec(str);
	});
}

void Core::launch()
{
	std::cout << "Lancement du thread..." << std::endl;
	this->graphicThread = boost::thread(boost::ref(this->window));
	std::cout << "On attends que le graphicThread finisse..." << std::endl;
	this->graphicThread.join();
	std::cout << "graphicThread terminé" << std::endl;
}

void Core::exec(const std::string &str)
{
	std::string cmd;
  std::vector<std::string> arguments;
	try
	{
		this->parse(str, cmd, arguments);
		this->commandHandler.call(cmd, arguments);
	}
	catch (std::invalid_argument &e)
	{
		std::cerr << "Invalid argument(s): " << e.what() << std::endl;
	}
	catch (std::exception &e)
	{
		std::cerr << "Core::exec raise: " << e.what() << std::endl;
	}
}

void Core::parse(const std::string &str, std::string &cmd, std::vector<std::string> &arguments) const
{
	boost::split(arguments, str, boost::is_any_of(" ,\t"));
	cmd = arguments[0];
  arguments.erase(arguments.begin());
}
