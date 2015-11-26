#include "Core.hpp"

Core::Core()
	:	dos()
{}

void Core::exec(const std::string &str) const
{
	std::string cmd;
	pathVector paths;

	this->parse(str, cmd, paths);
	this->dos.call(cmd, paths);
}

void Core::parse(const std::string &str, std::string &cmd, pathVector &paths) const
{
	std::vector<std::string> strs;

	boost::split(strs, str, boost::is_any_of(" ,\t"));
	cmd = strs[0];
	paths.assign(strs.begin() + 1, strs.end());
}
