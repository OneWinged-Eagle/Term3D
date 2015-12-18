#pragma once

#include <boost/assign.hpp>
#include <boost/filesystem.hpp>
#include <boost/filesystem/fstream.hpp>
#include <boost/algorithm/string.hpp>

#include <iostream>
#include <utility>
#include <map>
#include <stdexcept>
#include <string>

namespace fs = boost::filesystem;

typedef std::vector<fs::path> pathVector;
typedef std::vector<std::string> optionVector;

class CommandHandler
{
	typedef void (CommandHandler::*commandPtr)(const pathVector &, const optionVector &) const;
	typedef std::map<const std::string, commandPtr> commandMap;

private:
	const commandMap callMap = boost::assign::map_list_of
		("cat", &CommandHandler::cat)
		("cd", &CommandHandler::cd)
		("cp", &CommandHandler::cp)
		("ls", &CommandHandler::ls)
		("mv", &CommandHandler::mv)
		("pwd", &CommandHandler::pwd)
		("rm", &CommandHandler::rm)
		("touch", &CommandHandler::touch);
	const std::string baseDir = fs::current_path().string();

public:
	CommandHandler();
	void call(const std::string &cmd, const std::vector<std::string> &arguments) const;

private:
	std::pair <optionVector, pathVector> parseArguments(const std::vector<std::string> &arguments) const;
	void cat(const pathVector &paths, const optionVector &options) const;
	void cd(const pathVector &paths, const optionVector &options) const;
	void cp(const pathVector &paths, const optionVector &options) const;
	void ls(const pathVector &paths, const optionVector &options) const;
	void mv(const pathVector &paths, const optionVector &options) const;
	void pwd(const pathVector &paths, const optionVector &options) const;
	void rm(const pathVector &paths, const optionVector &options) const;
	void touch(const pathVector &paths, const optionVector &options) const;
};
