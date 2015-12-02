#pragma once

#include "CommandHandler.hpp"

#include <boost/algorithm/string.hpp>

class Core
{
private:
	CommandHandler commandHandler;

public:
	Core();

	void exec(const std::string &cmd) const;

private:
	void parse(const std::string &str, std::string &cmd, pathVector &paths) const;
};
