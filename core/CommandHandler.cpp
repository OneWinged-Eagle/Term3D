#include "CommandHandler.hpp"

CommandHandler::CommandHandler()
{}

void CommandHandler::call(const std::string &cmd, const pathVector &paths) const
{
  if (cmd.length() > 0 && this->callMap.find(cmd) != this->callMap.end())
	{
		try
		{
		  (this->*(callMap.at(cmd)))(paths);
		}
		catch (std::exception &e)
		{
			std::cerr << "CommandHandler::call raise: \"" << e.what() << "\" with command \"" << cmd << "\"." << std::endl;
		}
	}
	else
	{
		std::cerr << "Command \"" << cmd << "\" unknown." << std::endl;
	}
}

void CommandHandler::cat(const pathVector &paths) const
{
	if (paths.size() == 0)
		throw std::invalid_argument("\"cat\" command needs at least one file");

	for (const fs::path path : paths)
	{
		if (!fs::exists(path) || fs::is_directory(path))
		{
			std::cerr << "the file \"" << path << "\" doesn't exist." << std::endl;
			continue;
		}

		fs::ifstream file(path);

		if (file.peek() != std::ifstream::traits_type::eof())
			std::cout << file.rdbuf();
		file.close();
		std::cout << "\"" << path << "\" printed." << std::endl;
	}
}

void CommandHandler::cd(const pathVector &paths) const
{
	if (paths.size() != 1)
		throw std::invalid_argument("\"cd\" command needs a directory");

	const fs::path path(paths.at(0));

	if (!fs::exists(path) || !fs::is_directory(path))
		throw std::invalid_argument("the directory \"" + path.string() + "\" doesn't exist.");

	fs::current_path(path);
	this->pwd(paths);
}

void CommandHandler::cp(const pathVector &paths) const // TODO: gèrer les directories !
{
	if (paths.size() != 2)
		throw std::invalid_argument("\"cd\" command needs two files");

	const fs::path oldPath(paths.at(0)), newPath(paths[1]);

	if (!fs::exists(oldPath) || fs::is_directory(oldPath))
		throw std::invalid_argument("the file \"" + oldPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::copy(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" copied to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::ls(const pathVector &paths) const
{
	if (paths.size() == 0)
	{
		const fs::directory_iterator endIter;

		for (fs::directory_iterator dirIter("."); dirIter != endIter; ++dirIter)
			std::cout << dirIter->path().filename() << std::endl;
			std::cout << "\".\" listed." << std::endl;
	}
	else
		for (const fs::path path : paths)
		{
			if (!fs::exists(path) || !fs::is_directory(path))
			{
				std::cerr << "the file \"" << path << "\" doesn't exist." << std::endl;
				continue;
			}

			const fs::directory_iterator endIter;

			for (fs::directory_iterator dirIter(path); dirIter != endIter; ++dirIter)
				std::cout << dirIter->path().filename() << std::endl;
				std::cout << "\"" << path << "\" listed." << std::endl;
		}
}

void CommandHandler::mv(const pathVector &paths) const
{
	if (paths.size() != 2)
		throw std::invalid_argument("\"mv\" command needs two files");

	const fs::path oldPath(paths.at(0)), newPath(paths[1]);

	if (!fs::exists(oldPath))
		throw std::invalid_argument("the file \"" + oldPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::rename(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" renamed to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::pwd(const pathVector &paths) const
{
	(void)paths;
	std::cout << "The current working directory is: \"" << fs::current_path() << "\"" << std::endl;
}

void CommandHandler::rm(const pathVector &paths) const
{
	if (paths.size() == 0)
		throw std::invalid_argument("\"rm\" command needs at least one file");

	for (const fs::path path : paths)
	{
		if (!fs::remove(path))
			std::cout << "\"" << path << "\" doesn't exists." << std::endl;
			else
			std::cout << "\"" << path << "\" deleted." << std::endl;
	}
}

void CommandHandler::touch(const pathVector &paths) const
{
	if (paths.size() == 0)
		throw std::invalid_argument("\"touch\" command needs at least one file");

	for (const fs::path path : paths)
	{
		fs::ofstream file(path);

		file.close();
		std::cout << "\"" << path << "\" created." << std::endl;
	}
}
