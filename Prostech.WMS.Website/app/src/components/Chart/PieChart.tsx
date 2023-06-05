"use client";
import React from "react";
import Chart from "react-apexcharts";
function PieChart() {
  return (
    <React.Fragment>
      <div className="container-fluid">
        <h4>welcome to pie chart</h4>
        <Chart
          type="pie"
          width={450}
          options={{
            labels: ["aaaaaa", "bbbbbb", "vcdcsd", "fcdcsd"],
          }}
          series={[10, 23, 40, 50]}
        ></Chart>
      </div>
    </React.Fragment>
  );
}
export default PieChart;
