#include "Core.hpp"

Core::Core()
{
	this->dos = new DOS();
}

const void Core::exec(const std::str &str)
{
	std::str cmd;
	pathVector paths;

	this->parse(str, cmd, paths);
	this->dos.call(cmd, paths);
}

const void Core::parse(const std::string &str, std::string &cmd, pathVector &paths)
{
	std::vector<std::string> strs;

	boost::split(strs, str, boost::is_any_of(" ,\t"))
	cmd = strs[0];
	paths.assign(strs.begin() + 1, strs.end());
}
