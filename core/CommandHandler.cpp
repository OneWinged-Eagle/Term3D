#include "CommandHandler.hpp"

CommandHandler::CommandHandler()
{}

bool CommandHandler::call(const std::string &cmd, const std::vector<std::string> &arguments)
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
			return false;
		}
		catch (std::exception &e)
		{
			std::cerr << "Exception raised: \"" << e.what() << "\" with command \"" << cmd << "\"." << std::endl;
			return false;
		}
	return true;
}

std::pair <optionVector, pathVector> CommandHandler::parseArguments(const std::vector<std::string> &arguments)
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

void CommandHandler::whoami(const pathVector &paths, const optionVector &options)
{
	(void) options;
	(void) paths;
	char *user = secure_getenv("USER");
	// Voir si c'est pareil sur Windows et MacOS !
	if (user == NULL)
		throw std::invalid_argument("Argument not found in ENV");
	std::cout << "Result : " << std::string(user) << std::endl;
}

void CommandHandler::tree(const pathVector &paths, const optionVector &options)
{
	(void) options;
	(void) paths;

	std::cout << "Tree" << std::endl;
}

void CommandHandler::cat(const pathVector &paths, const optionVector &options)
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
//		std::cout << "\"" << path << "\" printed." << std::endl;
	}
}

void CommandHandler::cd(const pathVector &paths, const optionVector &options)
{
	if (paths.size() == 0 && options.size() == 0)
	{
		fs::current_path(this->baseDir);
		this->pwd(paths, options);
	}
	else
		if (options.size() != 0)
		{
			if (options.at(0) == "-")
			{
				if (!fs::exists(this->previousPath) || !fs::is_directory(this->previousPath))
					throw std::invalid_argument("the directory \"" + this->previousPath.string() + "\" doesn't exist.");
				fs::path path = fs::current_path();
				fs::current_path(this->previousPath);
				this->previousPath = fs::path(path);
				std::cout << "The current working directory is: \"" << fs::current_path() << "\"" << std::endl;
			}
		}
		else
		{
			const fs::path path(paths.at(0));
			if (!fs::exists(path) || !fs::is_directory(path))
				throw std::invalid_argument("the directory \"" + path.string() + "\" doesn't exist.");
			fs::path tmp(fs::current_path());
			fs::current_path(path);
			this->previousPath = tmp;
			this->pwd(paths, options);
		}
}

void CommandHandler::cp(const pathVector &paths, const optionVector &options) // TODO: gèrer les directories !
{
	(void) options;
	if (paths.size() != 2)
		throw std::invalid_argument("\"cp\" command needs two files");

	const fs::path previousPath(paths.at(0)), newPath(paths.at(1));

	if (!fs::exists(previousPath) || fs::is_directory(previousPath))
		throw std::invalid_argument("the file \"" + previousPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::copy(previousPath, newPath);
	std::cout << "\"" << previousPath << "\" copied to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::ls(const pathVector &paths, const optionVector &options)
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

void CommandHandler::mv(const pathVector &paths, const optionVector &options)
{
	(void) options;
	if (paths.size() != 2)
		throw std::invalid_argument("\"mv\" command needs two files or two directories");

	const fs::path previousPath(paths.at(0)), newPath(paths[1]);

	if (!fs::exists(previousPath))
		throw std::invalid_argument("the file \"" + previousPath.string() + "\" doesn't exist.");

	if (fs::exists(newPath))
		throw std::invalid_argument("the file \"" + newPath.string() + "\" already exists.");

	fs::rename(previousPath, newPath);
	std::cout << "\"" << previousPath << "\" renamed to \"" << newPath << "\"." << std::endl;
}

void CommandHandler::pwd(const pathVector &paths, const optionVector &options)
{
	(void) options;
	(void) paths;
	std::cout << "The current working directory is: \"" << fs::current_path() << "\"" << std::endl;
}

void CommandHandler::rm(const pathVector &paths, const optionVector &options)
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

void CommandHandler::touch(const pathVector &paths, const optionVector &options)
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
