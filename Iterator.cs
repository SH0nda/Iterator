using System;
using System.Collections.Generic;

namespace Test
{
	class Book
	{
		private string name;
		private int price;

		public Book(string name, int price)
		{
			this.name = name;
			this.price = price;
		}

		public string getName()
		{
			return name;
		}

		public int getPrice()
		{
			return price;
		}
	}

	interface Aggregate
	{
		Iterator createIterator();
	}

	class BookListAggregate : Aggregate
	{
		private List<Book> list = new List<Book>();
		private int numberOfStock;

		public Iterator createIterator()
		{
			return new BookListIterator(this);
		}

		public void add(Book book)
		{
			list.Add(book);
			numberOfStock++;
		}

		public object getAt(int number)
		{
			return list[number];
		}

		public int getNumberOfStock()
		{
			return numberOfStock;
		}
	}

	interface Iterator
	{
		void first();
		void next();
		bool isDone();
		object getItem();
	}

	class BookListIterator : Iterator
	{
		private BookListAggregate aggregate;
		private int current;

		public BookListIterator(BookListAggregate aggregate)
		{
			this.aggregate = aggregate;
		}

		public void first()
		{
			current = 0;
		}

		public void next()
		{
			current++;
		}

		public bool isDone()
		{
			if(current >= aggregate.getNumberOfStock())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public object getItem()
		{
			return aggregate.getAt(current);
		}
	}

	class main
	{
		public static void Main(string[] args)
		{
			BookListAggregate bookListAggregate = new BookListAggregate();
			Iterator iterator = bookListAggregate.createIterator();
			bookListAggregate.add(new Book("すごい本", 1000));
			bookListAggregate.add(new Book("楽しい本", 2000));
			bookListAggregate.add(new Book("悲しい本", 3000));
			bookListAggregate.add(new Book("面白い本", 4000));

			iterator.first();
			while (!iterator.isDone())
			{
				Book book = (Book)iterator.getItem();
				Console.WriteLine(book.getName() + ":" + book.getPrice() + "円");
				iterator.next();
			}
			Console.ReadLine();
		}
	}
}
