#pragma once

#include <boost/filesystem.hpp>
#include <iostream>
#include <map>
#include <string>

namespace fs = boost::filesystem;

typedef std::vector<fs::path> pathVector;
typedef void (DOS::*commandPtr)(const pathVector &);
typedef std::map<const std::string, commandPtr> commandMap;

class DOS
{
private:
	const commandMap callMap;
	fs::path current_path;

public:
	DOS();

	const void call(const std::string &cmd, const pathVector &paths);

private:
	const void cat(const pathVector &paths);
	const void cd(const pathVector &paths);
	const void cp(const pathVector &paths);
	const void ls(const pathVector &paths);
	const void mv(const pathVector &paths);
	const void pwd(const pathVector &paths);
	const void rm(const pathVector &paths);
	const void touch(const pathVector &paths);
};
