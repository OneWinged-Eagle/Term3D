#include "Core.hpp"

Core::Core()
 : commandHandler()
{}

void Core::exec(const std::string &str) const
{
	std::string cmd;
	pathVector paths;

	try
	{
		this->parse(str, cmd, paths);
		this->commandHandler.call(cmd, paths);
	}
	catch (std::exception &e)
	{
		std::cerr << "Core::exec raise: \"" << e.what() << "\"." << std::endl;
	}
}

void Core::parse(const std::string &str, std::string &cmd, pathVector &paths) const
{
	std::vector<std::string> strs;

	boost::split(strs, str, boost::is_any_of(" ,\t"));
	cmd = strs[0];
	paths.assign(strs.begin() + 1, strs.end());
}
