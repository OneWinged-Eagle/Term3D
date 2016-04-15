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
#include <cstdlib>

namespace fs = boost::filesystem;

typedef std::vector<fs::path> pathVector;

class CommandHandler
{
	typedef void (CommandHandler::*commandPtr)(const pathVector &, const optionVector &);
	typedef std::map<const std::string, commandPtr> commandMap;

private:
	const commandMap callMap = boost::assign::map_list_of
		("whoami", &CommandHandler::whoami)
		("tree", &CommandHandler::tree)
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
	bool call(const std::string &cmd, const std::vector<std::string> &arguments);
	fs::path getPreviousPath(void);

private:
	fs::path previousPath;

	std::pair <optionVector, pathVector> parseArguments(const std::vector<std::string> &arguments);
	void whoami(const pathVector &paths, const optionVector &options);
	void tree(const pathVector &paths, const optionVector &options);
 	void cat(const pathVector &paths, const optionVector &options);
	void cd(const pathVector &paths, const optionVector &options);
	void cp(const pathVector &paths, const optionVector &options);
	void ls(const pathVector &paths, const optionVector &options);
	void mv(const pathVector &paths, const optionVector &options);
	void pwd(const pathVector &paths, const optionVector &options);
	void rm(const pathVector &paths, const optionVector &options);
	void touch(const pathVector &paths, const optionVector &options);
};
