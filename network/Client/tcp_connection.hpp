#ifndef TCP_CONNECTION_HPP
#define TCP_CONNECTION_HPP

#include <iostream>
#include <ctime>
#include <string>
#include <boost/bind.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/enable_shared_from_this.hpp>
#include <boost/asio.hpp>
#include <boost/array.hpp>

class tcp_connection : public boost::enable_shared_from_this<tcp_connection>
{
public:
  tcp_connection(boost::asio::io_service& io_service);
  ~tcp_connection();

  typedef boost::shared_ptr<tcp_connection> pointer;

  static pointer		create(boost::asio::io_service& io_service);
  boost::asio::ip::tcp::socket&	socket();
  void				read();
  void				handle_read(const boost::system::error_code& error, size_t number_bytes_read);

  
private:
  boost::asio::ip::tcp::socket	m_socket;
  boost::array<char, 128>	m_network_buffer;
  
};

#endif
