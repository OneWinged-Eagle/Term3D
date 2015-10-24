#include "DOS.hpp"

DOS::DOS()
{
	this->callMap["cat"] = &DOS::cat;
	this->callMap["cd"] = &DOS::cd;
	this->callMap["cp"] = &DOS::cp;
	this->callMap["ls"] = &DOS::ls;
	this->callMap["mv"] = &DOS::mv;
	this->callMap["pwd"] = &DOS::pwd;
	this->callMap["rm"] = &DOS::rm;
	this->callMap["touch"] = &DOS::touch;
}

void DOS::call(const std::string &cmd)
{
	if (cmd.length() > 0 && this->callMap[cmd])
		(this->*(callMap[cmd]))();
}

void DOS::cat()
{
	std::cout << "cat" << std::endl;
}

void DOS::cd()
{
	std::cout << "cd" << std::endl;
}

void DOS::cp()
{
	std::cout << "cp" << std::endl;
}

void DOS::ls()
{
	std::cout << "ls" << std::endl;
}

void DOS::mv()
{
	std::cout << "mv" << std::endl;
}

void DOS::pwd()
{
	std::cout << "pwd" << std::endl;
}

void DOS::rm()
{
	std::cout << "rm" << std::endl;
	if (remove("suppr.test"))
		std::cout << "Y'a pas de fichier !" << std::endl;
}

void DOS::touch()
{
	std::cout << "touch" << std::endl;
}
