"use client";
import React, { useState } from "react";
import { Sidebar, Menu, MenuItem } from "react-pro-sidebar";
import "./styles.css";
import Link from "next/link";
import { usePathname } from "next/navigation";
import Image from "next/image";
import logo from "../assets/logoFull.png";

const SideBar = () => {
  const [menuCollapse, setMenuCollapse] = useState(false);
  const pathname = usePathname();
  //create a custom function that will change menucollapse state from false to true and true to false
  const menuIconClick = () => {
    //condition checking to change state from true to false and vice versa
    menuCollapse ? setMenuCollapse(false) : setMenuCollapse(true);
  };
  return (
    <Sidebar backgroundColor="#F4A4A4" collapsed={menuCollapse}>
      <div className="flex justify-end pt-5">
        <div onClick={menuIconClick}>
          {menuCollapse ? (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M12.75 15l3-3m0 0l-3-3m3 3h-7.5M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
              />
            </svg>
          ) : (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M11.25 9l-3 3m0 0l3 3m-3-3h7.5M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
              />
            </svg>
          )}
        </div>
      </div>
      <div className="flex justify-center pt-5 pb-10">
        <div>
          {menuCollapse ? (
            "Logo"
          ) : (
            <Image
              src={logo}
              width={200}
              height={100}
              alt="Picture of Logo full"
            />
          )}
        </div>
      </div>
      <Menu className={`${!menuCollapse && "flex justify-center"}`}>
        <Link href="/products">
          <MenuItem
            className={`mt-[20px] hover:bg-white hover:text-black hover:px-[5px] text-white  ${
              !menuCollapse && "rounded-[10px] hover:rounded-[10px]"
            } ${pathname === "/products" && "menu_item_active"}`}
            icon={
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                stroke="currentColor"
                className="w-6 h-6"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M9.568 3H5.25A2.25 2.25 0 003 5.25v4.318c0 .597.237 1.17.659 1.591l9.581 9.581c.699.699 1.78.872 2.607.33a18.095 18.095 0 005.223-5.223c.542-.827.369-1.908-.33-2.607L11.16 3.66A2.25 2.25 0 009.568 3z"
                />
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M6 6h.008v.008H6V6z"
                />
              </svg>
            }
          >
            Products
          </MenuItem>
        </Link>
        <Link href="/statistics">
          <MenuItem
            className={`mt-[20px] hover:bg-white hover:text-black hover:px-[5px] text-white ${
              !menuCollapse && "rounded-[10px] hover:rounded-[10px]"
            } ${pathname === "/statistics" && "menu_item_active"}`}
            icon={
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="currentColor"
                className="w-6 h-6"
              >
                <path
                  fill-rule="evenodd"
                  d="M2.25 13.5a8.25 8.25 0 018.25-8.25.75.75 0 01.75.75v6.75H18a.75.75 0 01.75.75 8.25 8.25 0 01-16.5 0z"
                  clip-rule="evenodd"
                />
                <path
                  fill-rule="evenodd"
                  d="M12.75 3a.75.75 0 01.75-.75 8.25 8.25 0 018.25 8.25.75.75 0 01-.75.75h-7.5a.75.75 0 01-.75-.75V3z"
                  clip-rule="evenodd"
                />
              </svg>
            }
          >
            Statistics
          </MenuItem>
        </Link>
      </Menu>
    </Sidebar>
  );
};

export default SideBar;
