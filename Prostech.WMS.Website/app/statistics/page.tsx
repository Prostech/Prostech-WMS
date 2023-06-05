"use client";
import Button from "@mui/material/Button";
import PieChart from "../src/components/Chart/PieChart";
import BarChart from "../src/components/Chart/BarChart";
import DateRange from "../src/components/DateRangePicker/DateRange";
import StatisticsTable from "../src/components/Table/StatisticsTable";
const Statistics = () => {
  return (
    <main className="flex flex-col min-h-screen p-5 bg-gray-200">
      <div className="flex flex-row justify-between">
        <div className="text-lg font-bold text-black ">Statistics</div>
      </div>
      <div className="w-full min-h-screen p-10 pb-20 text-black bg-white ">
        <div className="flex items-center justify-start w-full">
          <DateRange />
          <div className="pl-5">
            <Button size="small" variant="contained" color="success">
              Search
            </Button>
          </div>
        </div>
        <div className="flex items-center justify-center pt-10">
          <PieChart />
          <PieChart />
        </div>
        <div className="flex justify-center w-full pt-10">
          <BarChart />
        </div>
        <div className="flex justify-center w-full pt-10 ">
          <StatisticsTable />
        </div>
      </div>
    </main>
  );
};

export default Statistics;
