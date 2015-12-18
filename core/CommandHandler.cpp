#include "CommandHandler.hpp"

CommandHandler::CommandHandler()
{
}

void CommandHandler::call(const std::string &cmd, const std::vector<std::string> &arguments) const
{
	if (cmd.length() > 0 && this->callMap.find(cmd) != this->callMap.end())
		try
		{
			std::pair <optionVector, pathVector> p = this->parseArguments(arguments);
			(this->*(callMap.at(cmd)))(p.second, p.first);
		}
		catch (std::invalid_argument &e)
		{
			std::cerr << "Invalid argument(s): " << e.what() << std::endl;
		}
		catch (std::exception &e)
		{
			std::cerr << "Exception raised: \"" << e.what() << "\" with command \"" << cmd << "\"." << std::endl;
		}
	else
		std::cerr << "Command \"" << cmd << "\" unknown." << std::endl;
}

std::pair <optionVector, pathVector> CommandHandler::parseArguments(const std::vector<std::string> &arguments) const
{
	std::vector<std::string> options;
	std::vector<fs::path> paths;

	for (const std::string str : arguments)
		{
			if (str.at(0) == '-')
				options.push_back(str);
			else
				paths.push_back(fs::path(str));
		}
	return std::make_pair(options, paths);
}

void CommandHandler::cat(const pathVector &paths, const optionVector &options) const
{
	(void) options;
	if (paths.size() < 1)
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

void CommandHandler::cd(const pathVector &paths, const optionVector &options) const
{
	(void) options;
	if (paths.size() == 0)
		{
			fs::current_path(this->baseDir);
			this->pwd(paths, options);
		}
	else
	{
		const fs::path path(paths.at(0));

		if (!fs::exists(path) || !fs::is_directory(path))
			throw std::invalid_argument("the directory \"" + path.string() + "\" doesn't exist.");

		fs::current_path(path);
		this->pwd(paths, options);
	}
}

void CommandHandler::cp(const pathVector &paths, const optionVector &options) const // TODO: gèrer les directories !
{
	(void) options;
	if (paths.size() != 2)
		throw std::invalid_argument("\"cp\" command needs two files");

	const fs::path oldPath(paths.at(0)), newPath(paths.at(1));

	if (!fs::exists(oldPath) || fs::is_directory(oldPath))
		throw std::invalid_argument("the file \"" + oldPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::copy(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" copied to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::ls(const pathVector &paths, const optionVector &options) const
{
	(void) options;
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
				std::cerr << "the directory \"" << path << "\" doesn't exist." << std::endl;
				continue;
			}

			const fs::directory_iterator endIter;

			for (fs::directory_iterator dirIter(path); dirIter != endIter; ++dirIter)
				std::cout << dirIter->path().filename() << std::endl;
			std::cout << "\"" << path << "\" listed." << std::endl;
		}
}

void CommandHandler::mv(const pathVector &paths, const optionVector &options) const
{
	(void) options;
	if (paths.size() != 2)
		throw std::invalid_argument("\"mv\" command needs two files or two directories");

	const fs::path oldPath(paths.at(0)), newPath(paths[1]);

	if (!fs::exists(oldPath))
		throw std::invalid_argument("the file \"" + oldPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::rename(oldPath, newPath);
	std::cout << "\"" << oldPath << "\" renamed to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::pwd(const pathVector &paths, const optionVector &options) const
{
	(void) paths;
	(void) options;
	std::cout << "The current working directory is: \"" << fs::current_path() << "\"" << std::endl;
}

void CommandHandler::rm(const pathVector &paths, const optionVector &options) const
{
	(void) options;
	if (paths.size() < 1)
		throw std::invalid_argument("\"rm\" command needs at least one file or one directory");

	for (const fs::path path : paths)
		if (!fs::remove(path))
			std::cout << "\"" << path << "\" doesn't exists." << std::endl;
		else
			std::cout << "\"" << path << "\" deleted." << std::endl;
}

void CommandHandler::touch(const pathVector &paths, const optionVector &options) const
{
	(void) options;
	if (paths.size() < 1)
		throw std::invalid_argument("\"touch\" command needs at least one file");

	for (const fs::path path : paths)
	{
		fs::ofstream file(path);
		file.close();
		std::cout << "\"" << path << "\" created." << std::endl;
	}
}
