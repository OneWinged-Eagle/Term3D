#ifndef TCP_CLIENT_HPP
#define TCP_CLIENT_HPP

#include <iostream>
#include <ctime>
#include <string>
#include <boost/bind.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/enable_shared_from_this.hpp>
#include <boost/asio.hpp>
#include <boost/array.hpp>

class	tcp_client
{
public:
  tcp_client(boost::asio::io_service io_service, boost::asio::ip::tcp::endpoint endpoint);
  ~tcp_client();
  
private:
 
};

#endif
