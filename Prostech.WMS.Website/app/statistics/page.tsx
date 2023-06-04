import PieChart from "../src/components/Chart/PieChart";
const Statistics = () => {
  return (
    <main className="flex flex-col min-h-screen p-5 bg-gray-200">
      <div className="flex flex-row justify-between">
        <div className="text-lg font-bold text-black ">Statistics</div>
      </div>
      <div className="min-h-screen text-black bg-white">
        <PieChart />
      </div>
    </main>
  );
};

export default Statistics;
