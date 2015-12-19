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
#include "tcp_connection.hpp"

class	tcp_client
{
public:
  tcp_client(boost::asio::io_service& io_service, boost::asio::ip::tcp::endpoint& endpoint);
  ~tcp_client();

  void	connect(boost::asio::ip::tcp::endpoint& endpoint);
  void	handle_connect(tcp_connection::pointer new_connection, const boost::system::error_code& error);

private:
  boost::asio::io_service&	m_io_service;
  
};

#endif
