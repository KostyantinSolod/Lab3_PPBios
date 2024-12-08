class Consumer implements Runnable {
    private final Warehouse warehouse;
    private final String name;
    private final int consumeCount;

    public Consumer(Warehouse warehouse, String name, int consumeCount) {
        this.warehouse = warehouse;
        this.name = name;
        this.consumeCount = consumeCount;
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < consumeCount; i++) {
                warehouse.consume(name);
            }
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }
}