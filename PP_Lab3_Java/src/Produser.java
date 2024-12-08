class Producer implements Runnable {
    private final Warehouse warehouse;
    private final String name;
    private final int[] productionPlan;

    public Producer(Warehouse warehouse, String name, int[] productionPlan) {
        this.warehouse = warehouse;
        this.name = name;
        this.productionPlan = productionPlan;
    }

    @Override
    public void run() {
        try {
            for (int item : productionPlan) {
                warehouse.produce(item, name);
            }
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }
}