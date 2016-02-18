#define BOOST_TEST_MODULE commandTesting
#include <boost/test/included/unit_test.hpp>
#include "CommandHandler.hpp"

BOOST_AUTO_TEST_SUITE (commandTesting)

BOOST_AUTO_TEST_CASE (test1)
{
  std::vector<std::string> arg;
  CommandHandler commands;

  std::cout << std::endl;
  std::cout << "\t\033[1;31m Test1 \033[0m" << std::endl << std::endl;

  std::cout << "\033[1;34m Testing user : \033[0m";
  BOOST_CHECK(commands.call("whoami", arg));

  std::cout << "\033[1;34m Listing tree : \033[0m";
  BOOST_CHECK(commands.call("tree", arg));

  arg.push_back("test/test.txt");
  std::cout << "\033[1;34m Reading small file : \033[0m";
  BOOST_CHECK(commands.call("cat", arg));

  arg[0] = "test";
  std::cout << "\033[1;34m Moving to the \"test\" directory : \033[0m";
  BOOST_CHECK(commands.call("cd", arg));
  arg[0] = "..";
  std::cout << "\033[1;34m Moving to previous directory : \033[0m";
  BOOST_CHECK(commands.call("cd", arg));
  arg[0] = "-";
  std::cout << "\033[1;34m Moving back to \"test\" directory : \033[0m";
  BOOST_CHECK(commands.call("cd", arg));
  arg[0] = "test";
  std::cout << "\033[1;34m Moving to the \"test\" directory : \033[0m";
  BOOST_CHECK(commands.call("cd", arg));

  arg.pop_back();
  std::cout << "\033[1;34m Listing current repository : \033[0m" << std::endl;
  BOOST_CHECK(commands.call("ls", arg));
  std::cout << std::endl;
}

BOOST_AUTO_TEST_SUITE_END()
