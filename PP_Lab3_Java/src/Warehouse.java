import java.util.LinkedList;
import java.util.Queue;
import java.util.concurrent.Semaphore;

class Warehouse {
    private final Queue<Integer> storage;
    private final int capacity;

    private final Semaphore mutex; // Доступ до сховища
    private final Semaphore emptySlots; // Скільки вільних місць у сховищі
    private final Semaphore fullSlots; // Скільки зайнятих місць у сховищі

    public Warehouse(int capacity) {
        this.capacity = capacity;
        this.storage = new LinkedList<>();
        this.mutex = new Semaphore(1);
        this.emptySlots = new Semaphore(capacity);
        this.fullSlots = new Semaphore(0);
    }

    public void produce(int item, String producerName) throws InterruptedException {
        emptySlots.acquire(); // Очікування наявності вільного місця
        mutex.acquire(); // Ексклюзивний доступ до сховища

        storage.add(item);
        System.out.printf("%s produced %d. Warehouse: %s%n", producerName, item, storage);

        mutex.release();
        fullSlots.release(); // Повідомляємо, що є новий елемент
    }

    public int consume(String consumerName) throws InterruptedException {
        fullSlots.acquire(); // Очікування наявності елемента для споживання
        mutex.acquire(); // Ексклюзивний доступ до сховища

        int item = storage.poll();
        System.out.printf("%s consumed %d. Warehouse: %s%n", consumerName, item, storage);

        mutex.release();
        emptySlots.release(); // Повідомляємо, що стало вільне місце

        return item;
    }
}