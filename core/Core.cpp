#include "Core.hpp"

Core::Core()
 : commandHandler()
{}

void Core::exec(const std::string &str) const
{
	std::string cmd;
  std::vector<std::string> arguments;
	try
	{
		this->parse(str, cmd, arguments);
		this->commandHandler.call(cmd, arguments);
	}
	catch (std::exception &e)
	{
		std::cerr << "Core::exec raise: \"" << e.what() << "\"." << std::endl;
	}
}

void Core::parse(const std::string &str, std::string &cmd, std::vector<std::string> &arguments) const
{
	boost::split(arguments, str, boost::is_any_of(" ,\t"));
	cmd = arguments[0];
  arguments.erase(arguments.begin());
}
