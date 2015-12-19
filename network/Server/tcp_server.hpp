#ifndef TCP_SERVER_HPP
#define TCP_SERVER_HPP

#include <iostream>
#include <ctime>
#include <string>
#include <boost/bind.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/enable_shared_from_this.hpp>
#include <boost/asio.hpp>
#include <boost/array.hpp>
#include "tcp_connection.hpp"

class	tcp_server
{
public:
  tcp_server(boost::asio::io_service& io_service, int port);
  ~tcp_server();

  void	start_accept();
  void	handle_accept(tcp_connection::pointer new_connection, const boost::system::error_code& error);
  
private:
  boost::asio::ip::tcp::acceptor	m_acceptor;
};

#endif
