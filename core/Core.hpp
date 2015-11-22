#pragma once

#include "DOS.hpp"

#include <boost/algorithm/string.hpp>

class Core
{
private:
	DOS dos;

public:
	Core();

	const void exec(const std::str &cmd)
private:
	const void parse(const std::string &str, std::string &cmd, pathVector &paths)
};
