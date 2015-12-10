/*
 Tous les exemples sont tirés de https://bitbucket.org/gavinb/boost_samples/src/9e59a81d0079

 L'exemple 1 est tiré du dossier "threads" tandis que le 2 du dossier "mutexes".
 À voir aussi pour le dossier "condition", ça peut être intéressant des mutexes avec conditions...

 J'arrive pas trop à voir le point du dossier "memory", m'enfin...

 Ah, et pour les articles, c'est sur http://antonym.org/, ça date un peu mais la partie threads de boost n'a pas trop changé, voir pas du tout, donc bon.
*/

#include <stdio.h> // Pour printf. Pourquoi printf ? Parce que les threads aiment sauter std::cout de manière RAW.

#include <boost/thread.hpp>

/*EXEMPLE 2 AVEC UN MUTEX QUI ESSAYE DE BLOQUER*/
static const unsigned int kMaxSleepTime_ms = 500;

typedef std::vector<long> workqueue_t;
workqueue_t gWorkQueue;
boost::mutex gWorkQueueMutex;

bool gRunning;

void consumerThread()
{
	while (gRunning)
	{
		if (gWorkQueueMutex.try_lock())
		{
			if (gWorkQueue.size())
			{
				long val = gWorkQueue.back();
				gWorkQueue.pop_back();

				printf("vvv %ld\n", val);
			}
			boost::posix_time::milliseconds delayTime(50);
			boost::this_thread::sleep(delayTime);

			gWorkQueueMutex.unlock();
		}
		else
			printf("v==\n");

		boost::posix_time::milliseconds delayTime(random() % kMaxSleepTime_ms);
		boost::this_thread::sleep(delayTime);
	}
}

void producerThread()
{
	while (gRunning)
	{
		if (gWorkQueueMutex.try_lock())
		{
			long val = random();

			gWorkQueue.push_back(val);

			printf("^^^ %ld\n", val);

			boost::posix_time::milliseconds delayTime(15);
			boost::this_thread::sleep(delayTime);

			gWorkQueueMutex.unlock();
		}
		else
			printf("^==\n");

		boost::posix_time::milliseconds delayTime(random() % kMaxSleepTime_ms);
		boost::this_thread::sleep(delayTime);
	}
}

/*EXEMPLE 1 AVEC UN SIMPLE THREAD DANS UNE CLASSE*/
class Worker
{
private:
	boost::thread m_Thread;

public:
  Worker()
  {}

	void start(const int &N)
	{
		m_Thread = boost::thread(&Worker::processQueue, this, N);
	}

	void join()
	{
		m_Thread.join();
	}

	void processQueue(const unsigned int &N)
	{
		float ms = N * 1e3;
		boost::posix_time::milliseconds workTime(ms);

		std::cout << "Worker: started, will work for " << ms << "ms" << std::endl;

		boost::this_thread::sleep(workTime);

		std::cout << "Worker: completed" << std::endl;
	}
};

int main()
{
	/*EXEMPLE 1 AVEC UN SIMPLE THREAD DANS UNE CLASSE*/
	std::cout << "main: startup." << std::endl;

	Worker worker;

	worker.start(3);

	std::cout << "main: waiting for thread..." << std::endl;

	worker.join();

	std::cout << "main: done." << std::endl;

	//

	/*EXEMPLE 2 AVEC UN MUTEX QUI ESSAYE DE BLOQUER*/
	gRunning = true;

	boost::thread t1(producerThread);
	boost::thread t2(consumerThread);

	boost::posix_time::milliseconds workTime(5000);
	boost::this_thread::sleep(workTime);

	gRunning = false;
	t1.join();
	t2.join();

	return 0;
}
