#pragma once

#include <boost/signals2.hpp>

// Convinience wrapper for boost::signals2::signal.
template<typename Signature>
class Observer
{
private:
  template<typename Observers> friend class Observable;

  using Signal = boost::signals2::signal<Signature>;
  using SignalResult = typename Signal::result_type;

  Signal signal;

public:
  Observer(const Observer &observer) = delete;
  Observer &operator=(const Observer &observer) = delete;
  Observer() = default;
};

// Generic observable mixin - users must derive from it.
template<typename Observers>
class Observable
{
private:
  using ObserverTable = typename Observers::ObserverTable;

	ObserverTable signals;

protected:
  Observable() = default;

  // Notifies observers.
  template<size_t ObserverId, typename ... Args>	typename std::tuple_element<ObserverId, ObserverTable>::type::SignalResult Notify(Args &&... args) const
	{
    return std::get<ObserverId>(signals).signal(std::forward<Args>(args) ...);
  }
public:
  // Registers an observer.
  template<size_t ObserverId, typename F>
  boost::signals2::connection Register(F&& f)
	{
    return std::get<ObserverId>(signals).signal.connect(std::forward<F>(f));
  }
};

struct WindowObservers
{
  enum
	{
		Exec
	};

  using ObserverTable = std::tuple<Observer<void(const std::string &str)>>;
};
