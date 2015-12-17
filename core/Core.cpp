#include "Core.hpp"

Core::Core()
 : commandHandler(), window()
{
	this->window.Register<WindowObservers::Exec>([this](const std::string &str) {
		this->exec(str);
	});
}

void Core::launch()
{
/*	std::cout << "Creating signal..." << std::endl;
	//
	std::cout << "Signal 	created.\nCreating connection..." << std::endl;
	boost::signals2::connection connectionTest = sigTest.connect([](const std::string &str){std::cout << "Le mot de passe est : \"" << str << "\"" << std::endl;});
	std::cout << "Connection created.\nSending signal..." << std::endl;
	sigTest("Coucou !");
	std::cout << "Signal sended." << std::endl << std::endl;
*/
	std::cout << "Coucou..." << std::endl;
	this->graphicThread = boost::thread(boost::ref(this->window));
	std::cout << "On attends que le graphicThread finisse..." << std::endl;
	this->graphicThread.join();
	std::cout << "Allez, salut !" << std::endl;
}

void Core::exec(const std::string &str) const
{
	std::string cmd;
	pathVector paths;

	try
	{
		this->parse(str, cmd, paths);
		this->commandHandler.call(cmd, paths);
	}
	catch (std::exception &e)
	{
		std::cerr << "Core::exec raise: \"" << e.what() << "\"." << std::endl;
	}
}

void Core::parse(const std::string &str, std::string &cmd, pathVector &paths) const
{
	std::vector<std::string> strs;

	boost::split(strs, str, boost::is_any_of(" ,\t"));
	cmd = strs[0];
	paths.assign(strs.begin() + 1, strs.end());
}
