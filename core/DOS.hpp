#pragma once

#include <boost/assign.hpp>
#include <boost/filesystem.hpp>
#include <iostream>
#include <map>
#include <string>

namespace assign = boost::assign;
namespace fs = boost::filesystem;

typedef std::vector<fs::path> pathVector;

class DOS
{
  typedef const void (DOS::*commandPtr)(const pathVector &);
  typedef std::map<const std::string, commandPtr> commandMap;

private:
  const commandMap callMap = assign::map_list_of
    ("cat", &DOS::cat)
    ("cd", &DOS::cd)
    ("cp", &DOS::cp)
    ("ls", &DOS::ls)
    ("mv", &DOS::mv)
    ("pwd", &DOS::pwd)
    ("rm", &DOS::rm)
    ("touch", &DOS::touch);
	fs::path current_path;

public:
	DOS();

	void call(const std::string &cmd, const pathVector &paths);

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
