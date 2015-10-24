#pragma once

#include <iostream>
#include <map>
#include <string>

class DOS
{
private:
	std::map<const std::string, void (DOS::*)(void)> callMap;

public:
	DOS();

	void call(const std::string &cmd);

private:
	void cat();
	void cd();
	void cp();
	void ls();
	void mv();
	void pwd();
	void rm();
	void touch();
};
