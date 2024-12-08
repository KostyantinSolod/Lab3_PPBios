public class Main {
    public static void main(String[] args) {
    int warehouseCapacity = 5; // Місткість сховища
    int producersCount = 3;
    int consumersCount = 2;

    int[][] productionPlans = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
    };

    int[] consumptionPlans = {5, 4};

    Warehouse warehouse = new Warehouse(warehouseCapacity);

    Thread[] producers = new Thread[producersCount];
    for (int i = 0; i < producersCount; i++) {
        producers[i] = new Thread(new Producer(warehouse, "Producer P" + i, productionPlans[i]));
    }

    Thread[] consumers = new Thread[consumersCount];
    for (int i = 0; i < consumersCount; i++) {
        consumers[i] = new Thread(new Consumer(warehouse, "Consumer C" + i, consumptionPlans[i]));
    }

    for (Thread producer : producers) {
        producer.start();
    }
    for (Thread consumer : consumers) {
        consumer.start();
    }

    try {
        for (Thread producer : producers) {
            producer.join();
        }
        for (Thread consumer : consumers) {
            consumer.join();
        }
    } catch (InterruptedException e) {
        Thread.currentThread().interrupt();
    }

    System.out.println("All tasks completed.");
    }
}