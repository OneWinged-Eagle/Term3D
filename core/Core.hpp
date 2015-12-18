#pragma once

#include "CommandHandler.hpp"

class Core
{
private:
	CommandHandler commandHandler;

public:
	Core();

	void exec(const std::string &cmd) const;

private:
	void parse(const std::string &str, std::string &cmd, std::vector<std::string> &paths) const;
};
