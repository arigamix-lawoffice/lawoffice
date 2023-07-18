import { D3Type } from 'tessa';
import {
  ChartFunc,
  DataType,
  colorFunc,
  StrokeWidth,
  HoverAnimationDuration,
  LoadAnimationDuration
} from './common';

let hoverTimeout: number | undefined;
let textTimeout: number | undefined;
let onLoadedTimeout: number | undefined;

export const pieChartFunc: ChartFunc = (
  d3: D3Type,
  ref: SVGSVGElement,
  data: DataType | null,
  width: number,
  height: number,
  selectedRow: ReadonlyMap<string, any> | null,
  setSelectedRow: (row: ReadonlyMap<string, any>) => void,
  caption: string,
  init: boolean,
  palette: string,
  _selectedColor: string,
  onLoaded?: () => void
) => {
  if (!data) {
    return null;
  }

  const arcInnerRadius = Math.min(width, height) / 6;
  const arcOuterRadius = Math.min(width, height) / 2;

  const arc = d3
    .arc()
    .innerRadius(arcInnerRadius - 20)
    .outerRadius(arcOuterRadius - 40);

  const pieceOutDistance = 20;

  const arcLabelRadius = arcOuterRadius * 0.6;
  const arcLabel = d3.arc().innerRadius(arcLabelRadius).outerRadius(arcLabelRadius);

  const transformFunc = (d: any) => {
    const midAngle = (d.endAngle + d.startAngle) / 2;
    return `translate(${
      parseFloat(Math.cos(midAngle - Math.PI / 2).toFixed(2)) * pieceOutDistance
    },${parseFloat(Math.sin(midAngle - Math.PI / 2).toFixed(2)) * pieceOutDistance})`;
  };

  const pie = d3
    .pie()
    .sort(null)
    .value((d: any) => d.value);

  const chart = () => {
    const arcs = pie(data as any);

    const svg = d3.select(ref).attr('viewBox', [-width / 2, -height / 2, width, height] as any);

    svg
      .append('g')
      .append('text')
      .attr('text-anchor', 'middle')
      .attr('class', 'graph-caption')
      .append('tspan')
      .attr('x', 0)
      .attr('y', -height / 2 + 20)
      .text(caption);

    const g = svg
      .append('g')
      .attr('stroke', 'white')
      .attr('transform', 'translate(0,10)')
      .selectAll('path')
      .data(arcs)
      .join('path')
      .attr('stroke-width', StrokeWidth)
      .attr('fill', (_d, index) => color(index as any) as any);

    const setHoverActions = () => {
      clearTimeout(hoverTimeout as number);
      g.append('title').text((d: any) => `${d.data.name}: ${d.data.value.toLocaleString()}`);
      g.on('mouseenter', function (_d) {
        d3.select(this)
          .attr('stroke', 'white')
          .transition()
          .duration(HoverAnimationDuration)
          .attr('transform', transformFunc);
      })
        .on('mouseleave', function (d: any) {
          if (d.target.__data__.data.row === selectedRow) {
            return;
          }
          d3.select(this)
            .transition()
            .duration(HoverAnimationDuration)
            .attr('transform', 'translate(0,0)')
            .attr('stroke-width', StrokeWidth);
        })
        .on('click', function (d: any) {
          setSelectedRow(d.target.__data__.data.row);
        });
    };

    const setText = () => {
      clearTimeout(textTimeout as number);
      svg
        .append('g')
        .attr('transform', 'translate(0,10)')
        .attr('font-family', 'sans-serif')
        .attr('fill', 'white')
        .attr('text-anchor', 'middle')
        .selectAll('text')
        .data(arcs)
        .join('text')
        .attr('transform', (d: any) => `translate(${arcLabel.centroid(d)})`)
        .call(text =>
          text
            .append('tspan')
            .attr('x', 0)
            .attr('y', '0.7em')
            .text((d: any) => d.data.value)
        );
    };

    const onLoadedFunc = () => {
      clearTimeout(onLoadedTimeout as number);
      onLoaded && onLoaded();
    };

    if (!init) {
      const count = data.reduce((a, b) => a + b.value, 0);

      g.transition()
        .delay(function (_d, i) {
          let del = 0;
          for (let j = 0; j < i; j++) {
            del += data[j].value;
          }
          return (del / count) * LoadAnimationDuration - 10;
        })
        .duration((d, _i) => (d.value / count) * LoadAnimationDuration)
        .attrTween('d', function (d: any) {
          const i = d3.interpolate(d.startAngle + 0.1, d.endAngle);
          return function (t: any) {
            d.endAngle = i(t);
            return arc(d);
          };
        } as any);

      clearTimeout(hoverTimeout as number);
      hoverTimeout = setTimeout(setHoverActions, LoadAnimationDuration);
      clearTimeout(textTimeout as number);
      textTimeout = setTimeout(setText, LoadAnimationDuration);
      clearTimeout(onLoadedTimeout as number);
      onLoadedTimeout = setTimeout(onLoadedFunc, LoadAnimationDuration);
    } else {
      g.attr('d', arc as any);

      setHoverActions();
      setText();
    }

    g.attr('transform', (d: any) => {
      if (d.data.row === selectedRow) {
        return transformFunc(d);
      } else {
        return 'translate(0,0)';
      }
    });

    return svg.node();
  };

  const color = colorFunc(d3, data, palette);

  const svg = chart();

  return svg;
};
