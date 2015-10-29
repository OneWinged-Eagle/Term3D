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

	try
	{
		this->current_path = fs::current_path();
	}
	catch (std::exception &e)
	{
		std::cerr << e.what() << std::endl;
	}
}

const void call(const std::string &cmd, const pathVector &paths)
{
	if (cmd.length() > 0 && this->callMap[cmd])
	{
		try
		{
			(this->*(callMap[cmd]))(paths);
		}
		catch (std::exception &e)
		{
			std::cerr << e.what() << std::endl;
		}
	}
}

const void DOS::cat(const pathVector &paths)
{
	const fs::path path(paths[0]);
	const fs::ifstream file(path);

	std::cout << file.rdbuf();
	file.close();
	std::cout << "\"" << path << "\" printed." << std::endl;
}

const void DOS::cd(const pathVector &paths)
{
	const fs::path path(paths[0]);

	this->current_path = fs::current_path(path);
	std::cout << "The current working directory is: \"" << this->current_path << "\"" << std::endl;
}

const void DOS::cp(const pathVector &paths)
{
	const fs::path oldPath(paths[0]), newPath(paths[1]);

	fs::copy(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" copied to \"" << newPath << "\"." << std::endl;
}

const void DOS::ls(const pathVector &paths)
{
	const fs::path path(paths[0]);
	const fs::directory_iterator endIter;

	for (fs::directory_iterator dirIter(path); dirIter != endIter; ++dirIter)
		std::cout << dirIter->path().filename() << std::endl;
	std::cout << "\"" << path << "\" listed." << std::endl;
}

const void DOS::mv(const pathVector &paths)
{
	const fs::path oldPath(paths[0]), newPath(paths[1]);

	fs::rename(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" renamed to \"" << newPath << "\"." << std::endl;
}

const void DOS::pwd(const pathVector &paths)
{
	std::cout << "The current working directory is: \"" << this->current_path << "\"" << std::endl;
}

const void DOS::rm(const pathVector &paths)
{
	const fs::path path(paths[0]);

	if (!fs::remove(path))
		std::cout << "\"" << path << "\" doesn't exists." << std::endl;
	else
		std::cout << "\"" << path << "\" deleted." << std::endl;
}

const void DOS::touch(const pathVector &paths)
{
	const fs::path path(paths[0]);
	const fs::ofstream file(path);

	file.close();
	std::cout << "\"" << path << "\" created." << std::endl;
}
