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

class CommandHandler
{
	typedef void (CommandHandler::*commandPtr)(const pathVector &, const std::vector<std::string> &) const;
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
	std::pair <std::vector<std::string>, pathVector> parseArguments(const std::vector<std::string> &arguments) const;
	void cat(const std::vector<std::string> &options, const pathVector &paths) const;
	void cd(const std::vector<std::string> &options, const pathVector &paths) const;
	void cp(const std::vector<std::string> &options, const pathVector &paths) const;
	void ls(const std::vector<std::string> &options, const pathVector &paths) const;
	void mv(const std::vector<std::string> &options, const pathVector &paths) const;
	void pwd(const std::vector<std::string> &options, const pathVector &paths) const;
	void rm(const std::vector<std::string> &options, const pathVector &paths) const;
	void touch(const std::vector<std::string> &options, const pathVector &paths) const;
};
