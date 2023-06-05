"use client";
import React from "react";
import Chart from "react-apexcharts";
function BarChart() {
  const config = {
    series: [
      {
        name: "Import",
        data: [44, 55, 57, 56, 61, 58, 63, 60, 66],
      },
      {
        name: "Export",
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94],
      },
    ],
    options: {
      dataLabels: {
        offsetY: -25,
        style: {
          fontSize: "12px",
          colors: ["#304758"],
        },
      },

      xaxis: {
        categories: [
          "Feb",
          "Mar",
          "Apr",
          "May",
          "Jun",
          "Jul",
          "Aug",
          "Sep",
          "Oct",
        ],
      },

      yaxis: {
        title: {
          text: "Time(s)",
        },
      },
    },
  };
  return (
    <React.Fragment>
      <div className="container-fluid">
        <h4>welcome to bar chart</h4>
        <Chart
          type="bar"
          width={900}
          options={config.options}
          series={config.series}
        ></Chart>
      </div>
    </React.Fragment>
  );
}
export default BarChart;
