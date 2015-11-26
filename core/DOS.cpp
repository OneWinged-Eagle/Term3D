#include "DOS.hpp"

DOS::DOS()
{}

void DOS::call(const std::string &cmd, const pathVector &paths) const
{
  if (cmd.length() > 0 && this->callMap.at(cmd))
	{
		try
		{
		  (this->*(callMap.at(cmd)))(paths);
		}
		catch (std::exception &e)
		{
			std::cerr << e.what() << std::endl;
		}
	}
}

void DOS::cat(const pathVector &paths) const
{
	const fs::path path(paths.at(0));
	fs::ifstream file(path);

	std::cout << file.rdbuf();
	file.close();
	std::cout << "\"" << path << "\" printed." << std::endl;
}

void DOS::cd(const pathVector &paths) const
{
	const fs::path path(paths.at(0));

	fs::current_path(path);
	this->pwd(paths);
}

void DOS::cp(const pathVector &paths) const
{
	const fs::path oldPath(paths.at(0)), newPath(paths[1]);

	fs::copy(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" copied to \"" << newPath << "\"." << std::endl;
}

void DOS::ls(const pathVector &paths) const
{
	const fs::path path(paths.at(0));
	const fs::directory_iterator endIter;

	for (fs::directory_iterator dirIter(path); dirIter != endIter; ++dirIter)
		std::cout << dirIter->path().filename() << std::endl;
	std::cout << "\"" << path << "\" listed." << std::endl;
}

void DOS::mv(const pathVector &paths) const
{
	const fs::path oldPath(paths.at(0)), newPath(paths[1]);

	fs::rename(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" renamed to \"" << newPath << "\"." << std::endl;
}

void DOS::pwd(const pathVector &paths) const
{
	(void)paths;
	std::cout << "The current working directory is: \"" << fs::current_path() << "\"" << std::endl;
}

void DOS::rm(const pathVector &paths) const
{
	const fs::path path(paths.at(0));

	if (!fs::remove(path))
		std::cout << "\"" << path << "\" doesn't exists." << std::endl;
	else
		std::cout << "\"" << path << "\" deleted." << std::endl;
}

void DOS::touch(const pathVector &paths) const
{
	const fs::path path(paths.at(0));
	fs::ofstream file(path);

	file.close();
	std::cout << "\"" << path << "\" created." << std::endl;
}
